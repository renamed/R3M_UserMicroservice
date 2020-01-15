
using Microsoft.AspNetCore.Mvc;
using R3M_User_ApiModels.Usuario;
using R3M_User_ApiModels;
using System.Threading.Tasks;
using R3M_User_App.Interfaces;
using R3M_User_Domain.Apoio;
/// <summary>
/// Endpoints responsáveis por operações com novos e já existentes usuários
/// </summary>
[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioApp _usuarioApp;

    public UsuarioController(IUsuarioApp usuarioApp)
    {
        this._usuarioApp = usuarioApp;
    }


    /// <summary>
    /// Cria um novo usuário no sistema
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(NovoUsuario.Response), 200)]
    [ProducesResponseType(typeof(ErroGenerico), 400)]
    public async Task<IActionResult> CriarUsuario(NovoUsuario.Request request)
    {
        try
        {
            return Ok(await _usuarioApp.AdicionarUsuario(request));
        }
        catch (ValidationException valEx)
        {
            return BadRequest(new ErroGenerico { Mensagem = valEx.Message });
        }
    }

    /// <summary>
    /// Retorna um usuário pelo seu id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ObtemUsuarioResponse), 200)]
    [ProducesResponseType(typeof(ErroGenerico), 400)]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            return Ok(await _usuarioApp.Get(id));
        }
        catch (ValidationException valEx)
        {
            return BadRequest(new ErroGenerico { Mensagem = valEx.Message });
        }
    }

}