using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception 
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))//gönderilen validatör bi r validator değilse hata ver
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);//validatörün instancesini oluştur,
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];//validatörün çalışma tipini bul// product valitadörün basetipi abstrac validatör. onun generic tipi product sınıfı.
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);//parametreleri bul
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
} //**** invocation, metot demektir
