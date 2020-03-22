namespace VMRent.Models
{
    public class ExtendedVm : Vm
    {
        public string Comment { get; set; }

        public override Vm DeepClone()
        {
            return new ExtendedVm
            {
                Id = this.Id,
                Name = this.Name,
                Comment = this.Comment
            };
        }
    }
}