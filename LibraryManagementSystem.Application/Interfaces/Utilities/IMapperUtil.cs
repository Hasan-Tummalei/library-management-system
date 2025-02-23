namespace LibraryManagementSystem.Application.Interfaces.Utilities
{
    /// <summary>
    /// Represents the utility interface for mapping between different object types.
    /// </summary>
    public interface IMapperUtil
    {
        /// <summary>
        /// Maps a source object of type <typeparamref name="TSource"/> to a destination object of type <typeparamref name="TDestination"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the source object to map from.</typeparam>
        /// <typeparam name="TDestination">The type of the destination object to map to.</typeparam>
        /// <param name="source">The source object to map.</param>
        /// <returns>A destination object of type <typeparamref name="TDestination"/> that represents the mapped result.</returns>
        TDestination Map<TSource, TDestination>(TSource source);

        /// <summary>
        /// Maps a list of source objects of type <typeparamref name="TSource"/> to a list of destination objects of type <typeparamref name="TDestination"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the source objects to map from.</typeparam>
        /// <typeparam name="TDestination">The type of the destination objects to map to.</typeparam>
        /// <param name="source">The list of source objects to map.</param>
        /// <returns>A list of destination objects of type <typeparamref name="TDestination"/> that represents the mapped result.</returns>
        List<TDestination> MapList<TSource, TDestination>(List<TSource> source);
    }
}
