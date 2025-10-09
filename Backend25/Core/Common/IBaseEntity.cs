namespace Core.Common
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        DateTime CreateDateTime { get; set; }
        DateTime ModifyDateTime { get; set; }
        DateTime? DeleteDate { get; set; }
    }
}