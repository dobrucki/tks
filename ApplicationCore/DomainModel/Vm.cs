namespace VMRent.DomainModel
{
    public class Vm
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public virtual Vm DeepClone()
        {
            return new Vm
            {
                Id = this.Id,
                Name = this.Name
            };
        }
    }
}