
using Microsoft.AspNetCore.Mvc;
using R3M_User_ApiModels.Usuario;
using R3M_User_ApiModels;
using System.Threading.Tasks;
using R3M_User_App.Interfaces;
using R3M_User_Domain.Apoio;
using static R3M_User_ApiModels.Usuario.AtualizacaoSenha;
/// <summary>
/// Endpoints responsáveis por operações com novos e já existentes usuários
/// </summary>
[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioApp _usuarioApp;

    public UsuariosController(IUsuarioApp usuarioApp)
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
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErroGenerico), 400)]
    public async Task<IActionResult> CriarUsuario([FromBody] NovoUsuario.Request request)
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
    /// Gera um novo token a ser usado para troca de senha
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("token")]
    [ProducesResponseType(typeof(GeracaoTokenResponse), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErroGenerico), 400)]
    public async Task<IActionResult> GerarToken([FromBody] GeracaoTokenRequest request)
    {
        try
        {
            return Ok(await _usuarioApp.GerarToken(request));
        }
        catch (ValidationException valEx)
        {
            return BadRequest(new ErroGenerico { Mensagem = valEx.Message });
        }
    }

    /// <summary>
    /// Atualiza a senha de um usuário dado o seu token
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch]
    [Route("token")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErroGenerico), 400)]
    public async Task<IActionResult> AtualizarSenha([FromBody] AtualizacaoSenhaRequest request)
    {
        try
        {
            await _usuarioApp.AtualizarSenha(request);
            return Ok();
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

    /// <summary>
    /// Remove um elemento da base de dados pelo seu id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {

            if (await _usuarioApp.Delete(id))
                return Ok(); // 200
            return Ok(null); // 204
        }
        catch (ValidationException valEx)
        {
            return BadRequest(new ErroGenerico { Mensagem = valEx.Message });
        }
    }

    /// <summary>
    /// Atualiza um usuário já existente no sistema
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(typeof(AtualizarUsuarioResponse), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErroGenerico), 400)]
    public async Task<IActionResult> Update(int id, [FromBody] AtualizarUsuarioRequest usuario)
    {
        try
        {
            return Ok(await _usuarioApp.Atualizar(id, usuario));
        }
        catch (ValidationException valEx)
        {
            return BadRequest(new ErroGenerico { Mensagem = valEx.Message });
        }
    }
}