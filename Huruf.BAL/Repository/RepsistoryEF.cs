using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using Huruf.DAL;

public class RepsistoryEF<T> : IDisposable where T : class
{
    private static gulam786_HurufEntities _db;

    public RepsistoryEF()
    {
        _db = new gulam786_HurufEntities();
    }
    public RepsistoryEF(bool DisableLazyLoading)
    {
        _db = new gulam786_HurufEntities();
        _db.Configuration.LazyLoadingEnabled = DisableLazyLoading;
    }

    public List<T> GetList()
    {
        try
        {
            return _db.Set<T>().ToList();
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.InnerException.Message);
        }
    }
    public List<T> GetListBySelector(Expression<Func<T, bool>> Selector)
    {
        try
        {
            IQueryable<T> objResult = _db.Set<T>().Where(Selector);
            return objResult.ToList();
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.InnerException.Message);
        }
    }
    public T Save(T objT)
    {
        try
        {
            _db.Set<T>().Add(objT);
            _db.SaveChanges();
            return objT;
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.InnerException.Message);
        }
    }
    public T Update(T objT)
    {
        try
        {
            _db.Set<T>().Attach(objT);
            _db.Entry<T>(objT).State = EntityState.Modified;
            _db.SaveChanges();
            return objT;
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.InnerException.Message);
        }
        finally
        {
            Dispose();
        }
    }

    public void Delete(T objT)
    {
        try
        {
            _db.Set<T>().Attach(objT);
            _db.Entry(objT).State = EntityState.Deleted;
            _db.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.InnerException.Message);
        }
    }

    public void DeleteByExpression(Expression<Func<T, bool>> Selector)
    {
        try
        {
            IQueryable<T> objResult = _db.Set<T>().Where(Selector);
            foreach (T item in objResult)
            {
                _db.Entry<T>(item).State = EntityState.Deleted;
                _db.Set<T>().Remove(item);
            }
            _db.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.InnerException.Message);
        }
    }

    public void Dispose()
    {
        _db.Dispose();
    }

}

