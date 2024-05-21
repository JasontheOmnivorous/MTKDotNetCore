namespace MTKDotNetCore.PizzaApi.Queries
{
    public class PizzaQuery
    {
        // we could use const keyword here since this value won't change
        // but the problem is, we can't know which part of the code is using this query
        // so it's better to use getters just to make it only available to get/read the value
        public static string PizzaOrderQuery { get; } = @"select po.*, p.Pizza, p.Price from [dbo].[Tbl_PizzaOrder] po 
        inner join Tbl_Pizza p on po.PizzaId = p.PizzaId 
        where PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo";

        public static string PizzaOrderDetailQuery { get; } = @"select pod.*, pe.PizzaExtraName, pe.Price from [dbo].[Tbl_PizzaOrderDetail] pod 
        inner join Tbl_PizzaExtra pe on pod.PizzaExtraId = pe.PizzaExtraId 
        where PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo";


    }
}
