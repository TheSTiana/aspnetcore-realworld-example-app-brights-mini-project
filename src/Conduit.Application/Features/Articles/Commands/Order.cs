//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using Conduit.Application.Extensions;
//using Conduit.Application.Features.Articles.Queries;
//using Conduit.Application.Interfaces;

//using MediatR;

//namespace Conduit.Application.Features.Articles.Commands;
//public record OrderCommand(string Slug, bool PyhysicalCopy, string Email, string SnailMail) : IRequest<SingleArticleResponse>;

//public class OrderHandler : IRequestHandler<OrderCommand, SingleArticleResponse>
//{
//    private readonly IAppDbContext _context;
//    private readonly ICurrentUser _currentUser;
//    private readonly bool _physicalCopy;
//    private readonly string _email;
//    private readonly string _snailMail;

//    public OrderHandler(IAppDbContext context, ICurrentUser currentUser, bool physicalCopy, string email, string snailMail)
//    {
//        _context = context;
//        _currentUser = currentUser;
//        _physicalCopy = physicalCopy;
//        _email = email;
//        _snailMail = snailMail;
//    }

//    public async Task<SingleArticleResponse> Handle(OrderCommand request, CancellationToken cancellationToken)
//    {
//        var article = await _context.Articles
//            .FindAsync(x => x.Slug == request.Slug, cancellationToken);


//        article.Order(_currentUser.User!, _physicalCopy, _email, _snailMail);


//        await _context.SaveChangesAsync(cancellationToken);

//        return new SingleArticleResponse(article.Map(_currentUser.User));
//    }
//}