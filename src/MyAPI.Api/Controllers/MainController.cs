using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyAPI.Business.Interfaces;
using MyAPI.Business.Notifications;
using System;
using System.Linq;

namespace MyAPI.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        //validação de notificação de erro
        //validação de modelstate
        //validação da operacao de negócios
        private readonly INotifier _notifier;
        public MainController(INotifier notifier)
        {
            _notifier = notifier;
        }
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                NotifyInvalidModel(modelState);

            return CustomResponse();

        }
        protected ActionResult CustomResponse(object result = null)
        {
            if(ValidOperation())
            {
                return Ok(new
                {
                        success = true,
                        data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotifications().Select(x => x.Message)
            });
        }
        protected void NotifyInvalidModel(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(x => x.Errors);
            foreach (var e in errors)
            {
                var errorMsg = e.Exception == null ? e.ErrorMessage : e.Exception.Message;
                NotifyError(errorMsg);
            }
        }
        protected void NotifyError(string message)
        {
            _notifier.Handle(new Notification(message));
        }
        protected bool ValidOperation()
        {
            return !_notifier.HasNotifications();
        }
    }
}
