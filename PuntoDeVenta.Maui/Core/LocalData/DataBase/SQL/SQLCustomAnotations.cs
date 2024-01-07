namespace PuntoDeVenta.Maui.Core.LocalData.DataBase.SQL
{

    [AttributeUsage(AttributeTargets.Property)]
    public class OneToManyAttribute : Attribute
    {
        public CascadeOperation CascadeOperations { get; }


        public OneToManyAttribute(CascadeOperation CascadeOperations)
        {
            this.CascadeOperations = CascadeOperations;
        }
    }
    public enum CascadeOperation
    {
        None,
        Insert,
        Update,
        Delete,
        All
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKeyAttribute : Attribute
    {
        public Type ReferenceType { get; }

        public ForeignKeyAttribute(Type referenceType)
        {
            ReferenceType = referenceType;
        }
    }




}
