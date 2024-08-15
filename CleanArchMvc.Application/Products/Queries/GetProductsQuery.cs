using CleanArchMvc.Domain.Entites;
using MediatR;

namespace CleanArchMvc.Application.Products.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
