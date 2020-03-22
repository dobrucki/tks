namespace RepositoriesAdapters.Entities
{
    public class VmEnt
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public virtual VmEnt DeepClone()
        {
            return new VmEnt
            {
                Id = this.Id,
                Name = this.Name
            };
        }
    }
}