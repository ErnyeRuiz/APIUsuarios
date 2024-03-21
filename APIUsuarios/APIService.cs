using APIUsuariosDataAccess.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace APIUsuarios
{
    public static class APIService
    {
        //Mostrar lista de usuarios
        public static async Task<IResult> MostrarListaUsuarios(NgUsersContext context)
        {
            try
            {
                //se retorna la lista de usuarios
                return Results.Ok(await context.Users.ToListAsync());
            }
            catch (Exception)
            {
                //si algo sale mal se informa codigo 500
                return Response.InternalServerErrorResponse("Algo salió mal");
            }
        }
        //Agregar nuevo usuario
        public static async Task<IResult> NuevoUsuario(NgUsersContext context, Users user)
        {
            try
            {
                //Validaciones //cada usuario debe tener al menos nombre y correo
                if (user.UserName == null)
                {
                    return Response.BadRequestResponse("Debe ingresar el nombre");
                }
                else if (user.UserName.Length == 0)
                {
                    return Response.BadRequestResponse("Debe ingresar el nombre");
                }
                else if(user.Email == null)
                {
                    return Response.BadRequestResponse("Debe ingresar el correo");
                }
                else if (user.Email.Length == 0)
                {
                    return Response.BadRequestResponse("Debe ingresar el correo");
                }
                else if (!Business.Validations.ValidarEmail(user.Email)) //validar que el email sea valido
                {
                    return Response.BadRequestResponse("Correo inválido");
                }
                var correoYaExiste = await context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

                if (correoYaExiste!=null)
                {
                    return Response.BadRequestResponse("Ya existe un usuario con el correo ingresado");
                }
               //agregar el usuario a la bd
                await context.Users.AddAsync(user);
                // guardar cambios en la bd
                await context.SaveChangesAsync();
                //retornar codigo 201
                return Results.Created("users/",user);

            }catch(Exception)
            {
                //si algo sale mal se informa codigo 500
                return Response.InternalServerErrorResponse("Algo salió mal");
            }
        }
    }
}
