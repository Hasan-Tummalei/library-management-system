using LibraryManagementSystem.Application.Interfaces.Utilities;
using Mapster;
using MapsterMapper;

namespace LibraryManagementSystem.Infrastructure.Utilities.Mapping
{
    /// <summary>
    /// Utility class for performing object-to-object mappings using Mapster.
    /// Implements the <see cref="IMapperUtil"/> interface to provide mapping functionality.
    /// </summary>
    public class MapperUtil : IMapperUtil
    {
        /// <summary>
        /// Maps an object of type <typeparamref name="TSource"/> to an object of type <typeparamref name="TDestination"/>.
        /// Uses Mapster to perform the mapping.
        /// </summary>
        /// <typeparam name="TSource">The source type to map from.</typeparam>
        /// <typeparam name="TDestination">The destination type to map to.</typeparam>
        /// <param name="source">The source object to map.</param>
        /// <returns>The mapped destination object.</returns>
        TDestination IMapperUtil.Map<TSource, TDestination>(TSource source)
        {
            return source.Adapt<TDestination>();
        }

        /// <summary>
        /// Maps a list of objects of type <typeparamref name="TSource"/> to a list of objects of type <typeparamref name="TDestination"/>.
        /// Uses Mapster to perform the mapping.
        /// </summary>
        /// <typeparam name="TSource">The source type to map from.</typeparam>
        /// <typeparam name="TDestination">The destination type to map to.</typeparam>
        /// <param name="source">The list of source objects to map.</param>
        /// <returns>A list of mapped destination objects.</returns>
        List<TDestination> IMapperUtil.MapList<TSource, TDestination>(List<TSource> source)
        {
            return source.Adapt<List<TDestination>>();
        }
    }
}
