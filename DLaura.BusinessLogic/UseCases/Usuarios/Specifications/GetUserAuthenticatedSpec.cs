using Ardalis.Specification; // Importa la librería Ardalis Specification para usar el patrón de especificación en consultas.
using DLaura.Entities; // Importa las entidades, en este caso la entidad User.

namespace DLaura.BusinessLogic.UseCases.Usuarios.Specifications
{
    /// <summary>
    /// Especificación que define los criterios para obtener un usuario autenticado a través de su nombre de usuario y contraseña.
    /// </summary>
    /// <remarks>
    /// Esta clase extiende de <see cref="Specification{User}"/> y se utiliza para construir una consulta específica 
    /// que buscará un usuario por su nombre de usuario y contraseña en la base de datos. Además, incluye la propiedad 
    /// de rol del usuario para ser cargada junto con el usuario.
    /// </remarks>
    public class GetUserAuthenticatedSpec : Specification<User>
    {
        /// <summary>
        /// Constructor que establece los criterios de búsqueda del usuario autenticado.
        /// </summary>
        /// <param name="email">El nombre de usuario que se va a buscar.</param>
        /// <param name="password">La contraseña del usuario que se va a buscar.</param>
        public GetUserAuthenticatedSpec(string email, string password)
        {
            // Define la consulta para buscar el usuario por nombre de usuario y contraseña.
            Query.Where(u => u.Email == email && u.UserPassword == password);

            // Incluye la propiedad de rol del usuario en la consulta, cargándola junto con el usuario.
            Query.Include(u => u.Rol);
        }
    }
}
