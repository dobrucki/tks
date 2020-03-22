namespace RepositoriesAdapters.Entities
{
    public class ExtendedVmEnt : VmEnt
    {
        public string Comment { get; set; }

        public override VmEnt DeepClone()
        {
            return new ExtendedVmEnt
            {
                Id = this.Id,
                Name = this.Name,
                Comment = this.Comment
            };
        }
    }
}