using System;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract;

public interface IProductService
{
    IResult Add(Product product);
    IResult Update(Product product);
    IResult Delete(Product product);
    IDataResult<Product> GetProductById(int id);
    IDataResult<List<Product>> GetAll();
    IDataResult<List<Product>> GetAllByCategoryId(int categoryId);


    IResult TransactionalOperation(Product product);
}