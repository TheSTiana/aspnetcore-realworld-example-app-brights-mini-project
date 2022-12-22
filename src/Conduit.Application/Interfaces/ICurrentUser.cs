using Conduit.Domain.Entities;

namespace Conduit.Application.Interfaces;

public interface ICurrentUser
{
    User? User { get; }

    long Identifier { get; }

    bool IsAuthenticated { get; }

    Task SetIdentifier(long identifier);

    Task Fresh();

    Task LoadFollowing();
    Task LoadFavoriteArticles();
}