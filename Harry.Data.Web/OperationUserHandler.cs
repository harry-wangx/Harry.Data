using Harry.Data.Entities.Auditing;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Harry.Data.Web
{
    public class OperationUserHandler<TUserKey> : IDataHandler
    {
        private readonly string userId;
        private readonly string userName;
        private readonly Func<string, TUserKey> getUserIdFunc;
        public OperationUserHandler(IHttpContextAccessor httpContextAccessor, Func<string, TUserKey> getUserIdFunc)
        {
            var httpContext = httpContextAccessor.HttpContext;
            this.getUserIdFunc = getUserIdFunc ?? throw new ArgumentNullException(nameof(getUserIdFunc));

            this.userId = httpContext?.User?.Claims?.FirstOrDefault(m => m.Type == ClaimTypes.NameIdentifier)?.Value;
            this.userName = httpContext?.User.Identity?.Name;


        }
        public int Order => 0;

        public void OnInserting(object entity)
        {
            if (entity == null)
                return;

            if (entity is ICreationAudited<TUserKey> model)
            {
                if (!EqualityComparer<TUserKey>.Default.Equals(model.CreatorId, default))
                {
                    //已经设置了创建者Id
                    return;
                }
                model.CreatorId = getUserIdFunc.Invoke(userId);
                model.CreatorName = this.userName;
            }
        }

        public void OnUpdating(object entity)
        {
            if (entity == null)
                return;

            if (entity is IModificationAudited<TUserKey> model)
            {
                model.LastModifierId = getUserIdFunc.Invoke(userId);
                model.LastModifierName = this.userName;
            }
        }

        public void OnDeleting(object entity)
        {
            if (entity == null)
                return;

            if (entity is IDeletionAudited<TUserKey> model)
            {
                model.DeleterId = getUserIdFunc.Invoke(userId);
                model.DeleterName = this.userName;
            }
        }
    }
}
