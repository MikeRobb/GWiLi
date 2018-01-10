namespace GWiLi.EntityFramework
{
    public partial class Category
    {
        public string GetParentalDisplayName()
        {
            var result = DisplayName;

            var c = this;
            while (c.HasParent())
            {
                result = $"{c.Parent.DisplayName} > {result}";
                c = c.Parent;
            }

            return result;
        }

        public bool HasParent()
        {
            return ParentCategoryId.HasValue;
        }
    }
}