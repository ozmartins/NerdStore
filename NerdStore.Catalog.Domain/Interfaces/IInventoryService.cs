namespace NerdStore.Catalog.Domain.Interfaces
{
    public interface IInventoryService
    {
        Task ReplenishStock(Guid productId, int quantity);

        Task WithdrawFromStock(Guid productId, int quantity);
    }
}
