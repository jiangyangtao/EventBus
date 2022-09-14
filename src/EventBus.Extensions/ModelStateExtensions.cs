using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EventBus.Extensions
{
    public static class ModelStateExtensions
    {
        /// <summary>
        /// 获取验证消息
        /// </summary>
        public static string GetValidationSummary(this ModelStateDictionary modelState)
        {
            if (modelState.IsValid) return null;

            var state = modelState.Values.FirstOrDefault(a => a.Errors.Count > 0);
            var message = state.Errors.FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.ErrorMessage))?.ErrorMessage;
            if (string.IsNullOrWhiteSpace(message))
            {
                message = state.Errors.FirstOrDefault(o => o.Exception != null)?.Exception.Message;
            }

            return message;
        }
    }
}
