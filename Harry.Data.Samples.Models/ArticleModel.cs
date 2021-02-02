using Harry.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Harry.Data.Samples.Models
{
    [Table("Articles")]
    public class ArticleModel : AuditedEntity<int, int>
    {
        public string Title { get; set; }

        public static ArticleModel Article1 { get; } = new ArticleModel() { Id = 1, Title = "Article1" };

        public static ArticleModel Article2 { get; } = new ArticleModel() { Id = 2, Title = "Article2" };
    }
}
