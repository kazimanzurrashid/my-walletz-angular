namespace MyWalletz.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using DataAccess;
    using DomainObjects;

    public interface ISeedDataLoader
    {
        void Load(string userName);
    }

    public class SeedDataLoader : ISeedDataLoader
    {
        private static readonly Lazy<ICollection<DefaultCategory>>
            DefaultCategories;

        private readonly Func<IDataContext> lazyDataContext;

        static SeedDataLoader()
        {
            var dataFile = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "App_Data",
                "seed.xml");

            XDocument doc;

            using (var stream = File.OpenRead(dataFile))
            {
                doc = XDocument.Load(stream);
            }

            var categories = doc.Root == null ?
                new List<DefaultCategory>() :
                LoadCategories(doc.Root);
 
            DefaultCategories = new Lazy<ICollection<DefaultCategory>>(
                () => categories);
        }

        public SeedDataLoader(Func<IDataContext> lazyDataContex)
        {
            this.lazyDataContext = lazyDataContex;
        }

        public void Load(string userName)
        {
            using (var dataContext = lazyDataContext())
            {
                var user = dataContext.Users
                    .FirstOrDefault(u => u.Email == userName);

                if (user == null)
                {
                    return;
                }

                foreach (var category in DefaultCategories.Value)
                {
                    CreateCategory(dataContext, category, user);
                }

                dataContext.SaveChanges();
            }
        }

        private static ICollection<DefaultCategory> LoadCategories(XContainer container)
        {
            var result = new List<DefaultCategory>();
            var categories = container.Element("categories");

            TraverseCategories(result, categories, "expenses", CategoryType.Expense);
            TraverseCategories(result, categories, "incomes", CategoryType.Income);

            return result;
        }

        private static void TraverseCategories(
            ICollection<DefaultCategory> list,
            XContainer parent,
            string elementName,
            CategoryType type)
        {
            var element = parent.Element(elementName);

            if (element == null)
            {
                return;
            }

            foreach (var category in element.Elements("category")
                .Select(e => TraverseCategory(e, type))
                .Where(category => category != null))
            {
                list.Add(category);
            }
        }

        private static DefaultCategory TraverseCategory(
            XElement element,
            CategoryType type)
        {
            var title = element.Attribute("title");

            if ((title == null) || string.IsNullOrWhiteSpace(title.Value))
            {
                return null;
            }

            var category = new DefaultCategory
            {
                Title = title.Value,
                Type = type,
            };

            return category;
        }

        private static void CreateCategory(
            IDataContext dataContext,
            DefaultCategory defaultCategory,
            User user)
        {
            var category = new Category
            {
                Title = defaultCategory.Title,
                Type = defaultCategory.Type,
                User = user
            };

            dataContext.Categories.Add(category);
        }

        private sealed class DefaultCategory
        {
            public string Title { get; set; }

            public CategoryType Type { get; set; }
        }
    }
}