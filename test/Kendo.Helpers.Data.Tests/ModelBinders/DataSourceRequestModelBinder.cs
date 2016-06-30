using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Xunit.Abstractions;
using System.Linq.Expressions;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using static Moq.MockBehavior;
using Xunit;

namespace Kendo.Helpers.Data.Tests.ModelBinders
{
    public class DataSourceRequestModelBinderTests : IDisposable
    {

        private ITestOutputHelper _outputHelper;
        private Mock<HttpRequest> _requestMock;
        private Mock<IValueProvider> _valueProviderMock;

        public static IEnumerable<object[]> GetQueryCases
        {
            get
            {
                yield return new object[]
                {
                    "http://localhost:5000?page=1&pageSize=10",
                    (((Expression<Func<ModelBindingContext, bool>>) (bindingContext =>
                        bindingContext.Result.Model != null
                        && bindingContext.Result.Model.GetType() == typeof(DataSourceRequest)
                        && ((DataSourceRequest)(bindingContext.Result.Model)).Page == 1
                        && ((DataSourceRequest)(bindingContext.Result.Model)).PageSize == 10
                    )))
                };
            }
        }


        public DataSourceRequestModelBinderTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _requestMock = new Mock<HttpRequest>();
            _valueProviderMock = new Mock<IValueProvider>();

        }


        [Theory]
        [MemberData(nameof(GetQueryCases))]
        public async Task BindingFromGetQuery(string query, Expression<Func<ModelBindingContext, bool>> bindingContextExpectation)
        {

            #region Arrange

            Uri uri = new Uri(query);
            Mock<IFormCollection> formCollectionMock = new Mock<IFormCollection>(Strict);

            

            //ModelBindingContext mbc = new DefaultModelBindingContext()
            //{
            //    ValueProvider = _valueProviderMock.Object,
            //    FieldName = "someField",
            //    IsTopLevelObject = true
            //};


            //#endregion
            //ComplexTypeModelBinder binder = new DataSourceRequestModelBinder(n);

            //await binder.BindModelAsync(mbc);

            //#region Assert

            //mbc.Should()
            //    .Match(bindingContextExpectation);

            #endregion
        }


        [Fact]
        public void InvalidBindModelAsyncWithNullBindingContextShouldThrowsArgumentException()
        {
            //IModelBinder binder = new DataSourceRequestModelBinder();
            //Func<Task> task = async () => await binder.BindModelAsync(null);

            //task.ShouldThrow<ArgumentNullException>()
            //    .And.Message.Should().NotBeNullOrWhiteSpace();
        }


        public void BindingFromPostQuery(string postContent, Expression<Func<ModelBindingContext, bool>> bindingContextExpectation)
        {

        }

        public void Dispose()
        {
            _outputHelper = null;
            _requestMock = null;
            _valueProviderMock = null;
            
        }
    }
}
