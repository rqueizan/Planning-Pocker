using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.ModelBinders
{
    public class FileFormDataModelBinder : IModelBinder
    {
        private readonly FormFileModelBinder formFileModelBinder;

        public FileFormDataModelBinder()
        {
            formFileModelBinder = new FormFileModelBinder(null);
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);
            if (valueResult == ValueProviderResult.None)
            {
                var message = bindingContext.ModelMetadata.ModelBindingMessageProvider.MissingBindRequiredValueAccessor(bindingContext.FieldName);
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, message);
                return;
            }
            var rawValue = valueResult.FirstValue;
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var model = JsonSerializer.Deserialize(rawValue, bindingContext.ModelType, options);
            foreach (ModelMetadata property in bindingContext.ModelMetadata.Properties)
            {
                if (property.ModelType != typeof(IFormFile)
                    && property.ModelType != typeof(IFormFile[])
                    && property.ModelType != typeof(List<IFormFile>))
                    continue;
                var fieldName = property.BinderModelName ?? property.PropertyName;
                var modelName = fieldName;
                var propertyModel = property.PropertyGetter(bindingContext.Model);
                ModelBindingResult propertyResult;
                using (bindingContext.EnterNestedScope(property, fieldName, modelName, propertyModel))
                {
                    await formFileModelBinder.BindModelAsync(bindingContext).ConfigureAwait(false);
                    propertyResult = bindingContext.Result;
                }
                if (propertyResult.IsModelSet)
                    property.PropertySetter(model, propertyResult.Model);
                else if (property.IsBindingRequired)
                {
                    var message = property.ModelBindingMessageProvider.MissingBindRequiredValueAccessor(fieldName);
                    bindingContext.ModelState.TryAddModelError(modelName, message);
                }
            }
            bindingContext.Result = ModelBindingResult.Success(model);
        }
    }
}
