namespace MyWalletz.DataTransferObjects
{
    using System;

    [Serializable]
    public class Category
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public CategoryType Type { get; set; }
    }
}