using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class ProductManager : IProductService
{
    private readonly IProductDal _productDal;
    private readonly ICategoryService _categoryService;

    public ProductManager(IProductDal productDal, ICategoryService categoryService)
    {
        _productDal = productDal;
        _categoryService = categoryService;
    }


    //Validasyon kuralları içerisinde biz bir third party kullanacağız, third party kullandığınız gördüğünüz an onu soyutlama ve onu klasörlerme yöntemlerini aklımızdan çıkarmıyoruz, neden third party bugün a yarın b third partysini kullanabiliriz.

    //Cross Cutting Concerns - Validation, Cache, Log, Performance, Auth, Transaction
    //AOP - Aspect Oriented Programming - Yazılım Geliştirme Yaklaşımı
    //AOP Cross Cutting Concerns için kullanılır, Cross Cutting Concernlerin dışında da kullanılmamalıdır

    [ValidationAspect(typeof(ProductValidator))]
    [CacheRemoveAspect("IProductService.Get")]
    public IResult Add(Product product)
    {
        //Validation: Doğrulama, ilgili parametredeki nesnesinin (product) iş kurallarına dahil etmek için uygunluğunu anlatır.
        //Business Codes

        IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                                           CheckIfCategoryIsEnabled());

        if (result != null)
        {
            return result;
        }

        _productDal.Add(product);

        return new SuccessResult(Messages.ProductAdded);
    }

    public IResult Delete(Product product)
    {
        _productDal.Delete(product);

        return new SuccessResult(Messages.ProductDeleted);
    }

    [PerformanceAspect(5)]
    public IDataResult<List<Product>> GetAll()
    {
        Thread.Sleep(5000);

        return new SuccessDataResult<List<Product>>(_productDal.GetAll().ToList());
    }

    //[SecuredOperation("Product.List,Admin")]
    //[CacheAspect(duration: 10)]
    [LogAspect(typeof(DatabaseLogger))]
    public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
    {
        return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryID == categoryId).ToList());
    }

    public IDataResult<Product> GetProductById(int id)
    {
        return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductID == id));
    }

    [TransactionScopeAspect]
    public IResult TransactionalOperation(Product product)
    {
        _productDal.Update(product);
        //_productDal.Add(product);

        return new SuccessResult(Messages.ProductUpdated);
    }

    public IResult Update(Product product)
    {
        _productDal.Update(product);

        return new SuccessResult(Messages.ProductUpdated);
    }

    private IResult CheckIfProductNameExists(string productName)
    {
        var product = _productDal.Get(p => p.ProductName.Equals(productName));

        if (product != null)
        {
            return new ErrorResult(Messages.ProductNameAlreadyExists);
        }

        return new SuccessResult();
    }

    private IResult CheckIfCategoryIsEnabled()
    {
        var result = _categoryService.GetAll();

        if(result.Data.Count < 10)
        {
            return new ErrorResult(Messages.ProductNameAlreadyExists);
        }

        return new SuccessResult();
    }
}
