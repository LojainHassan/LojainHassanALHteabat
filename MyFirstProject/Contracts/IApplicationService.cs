using MyFirstProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstProject.Contracts
{
    public interface IApplicationService<T>
    {
        /// <summary>
        /// Adds a new entity to the system.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity if found, otherwise null.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves all entities in the system.
        /// </summary>
        /// <returns>A list of all entities.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Updates an existing entity in the system.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to be updated.</param>
        /// <param name="entity">The updated entity data.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        Task<bool> UpdateAsync(int id, T entity);

        /// <summary>
        /// Deletes an entity from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to be deleted.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        Task<bool> DeleteAsync(int id);

        Task<bool> CreateRangeAsync(IEnumerable<T> entities);

    }
}
