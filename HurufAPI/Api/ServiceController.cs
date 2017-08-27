using Huruf.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Huruf.BAL;
using System.Transactions;
using Huruf.BAL.Repository;
namespace HurufAPI.Api
{
    public class ServiceController : ApiController
    {

        #region [Registration/Login]

        [HttpPost]
        [Route("RegisterUser")]
        public void RegisterUser(UserRegister obj)
        {
            if (obj != null)
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    try
                    {
                        using (RepsistoryEF<UserRegister> _o = new global::RepsistoryEF<UserRegister>())
                        {
                            UserRegister ur = null;
                            obj.CreateDate = DateTime.Now;
                            if (obj.RegistrationID > 0)
                            {
                                ur = _o.Update(obj);
                            }
                            else
                            {
                                ur = _o.Save(obj);
                            }

                            ReturnValues result = null;
                            if (ur != null)
                            {
                                result = new ReturnValues
                                {
                                    Success = "Registeration Successfully Done ",
                                };
                            }
                            trans.Complete();
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Dispose();
                        ReturnValues objex = new ReturnValues
                        {
                            Failure = ex.Message,

                        };
                        throw ex;
                    }
                    finally
                    {
                        trans.Dispose();
                    }
                }
            }
        }

        [HttpPost]
        [Route("LoginUser")]
        public ReturnValues LoginUser(Login obj)
        {
            try
            {
                ReturnValues result = null;
                using (RepsistoryEF<UserRegister> _o = new RepsistoryEF<UserRegister>())
                {
                    var resultValue = _o.GetListBySelector(z => z.UserName == obj.UserName.Trim() && z.Password == obj.Password.Trim()).FirstOrDefault();

                    if (resultValue != null)
                    {
                        if (obj.GCMId != null && obj.GCMId != string.Empty)
                        {
                            resultValue.GCMId = obj.GCMId;
                            _o.Update(resultValue);
                        }
                        result = new ReturnValues
                        {
                            Success = "Login Successfully",
                            Source = resultValue.RegistrationID.ToString(),
                        };
                    }
                    else
                    {
                        result = new ReturnValues
                        {
                            Success = "Login Failed, Please enter correct username and password",
                            Source = "0",
                        };
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                ReturnValues objex = new ReturnValues
                {
                    Failure = ex.Message,

                };
                throw ex;
            }
            finally
            {

            }

        }

        [HttpPost]
        [Route("ChangePassword")]
        public ReturnValues ChangePassword(ChangeUserPassword obj)
        {
            try
            {
                ReturnValues result = null;
                if (obj != null)
                {
                    using (RepsistoryEF<UserRegister> _o = new RepsistoryEF<UserRegister>())
                    {
                        var resultValue = _o.GetListBySelector(z => z.RegistrationID == obj.RegistrationID).FirstOrDefault();

                        if (resultValue != null)
                        {
                            if (resultValue.Password == obj.OldPassword)
                            {
                                resultValue.Password = obj.NewPassword;
                                _o.Update(resultValue);
                                result = new ReturnValues
                                {
                                    Success = "Password Changed Successfully",
                                    Source = resultValue.RegistrationID.ToString(),
                                };
                            }
                            else
                            {
                                result = new ReturnValues
                                {
                                    Success = "Old password is not correct.<br/>Please try again.",
                                    Source = resultValue.RegistrationID.ToString(),
                                };
                            }
                        }
                        else
                        {
                            result = new ReturnValues
                            {
                                Success = "Password could not be changed.",
                                Source = "0",
                            };
                        }
                    }

                }
                return result;
            }
            catch (Exception ex)
            {
                ReturnValues objex = new ReturnValues
                {
                    Failure = ex.Message,

                };
                throw ex;
            }
            finally
            {

            }

        }

        [HttpGet]
        [Route("GetUserInfo")]
        public UserRegister GetUserInfo(int UserID)
        {
            UserRegister user = null;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    if (UserID != 0)
                    {
                        using (RepsistoryEF<UserRegister> _o = new RepsistoryEF<UserRegister>())
                        {
                            user = _o.GetListBySelector(x => x.RegistrationID.Equals(UserID)).FirstOrDefault();
                        }
                    }

                    trans.Complete();
                    return user;
                }
                catch (Exception ex)
                {
                    trans.Dispose();
                    throw ex;
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }
        #endregion

        [HttpPost]
        [Route("SaveUserFile")]
        public ReturnValues SaveUserFile(AppFile obj)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    ReturnValues ret = null;
                    using (RepsistoryEF<AppFile> _o = new RepsistoryEF<AppFile>())
                    {
                        obj.IsDownloaded = false;
                        obj.Timestamp = DateTime.Now;
                        var result = _o.Save(obj);
                        if (result != null)
                        {
                            ret = new ReturnValues
                            {
                                Success = "File has been saved successfully.",
                                Source = "0",
                            };
                        }
                        else
                        {
                            ret = new ReturnValues
                            {
                                Success = "Failed to save",
                                Source = "0",
                            };
                        }
                        trans.Complete();
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    trans.Dispose();
                    ReturnValues objex = new ReturnValues
                    {
                        Failure = ex.Message,
                    };
                    throw ex;
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }

        [HttpGet]
        [Route("GetFiles")]
        public List<AppFile> GetFiles(int FileType, int FileCategory)
        {
            List<AppFile> files = null;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    if (FileType != 0 && FileCategory != 0)
                    {
                        using (RepsistoryEF<AppFile> _o = new RepsistoryEF<AppFile>())
                        {
                            files = _o.GetListBySelector(x => x.FileType.Equals(FileType) && x.FileCategory.Equals(FileCategory)).ToList();
                        }
                    }

                    trans.Complete();
                    return files;
                }
                catch (Exception ex)
                {
                    trans.Dispose();
                    throw ex;
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }

    }
}



