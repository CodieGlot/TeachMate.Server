using LLMSharp.Google.Palm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachMate.Domain;
using TeachMate.Domain.DTOs.SearchDto;
using TeachMate.Services;

namespace TeachMate.Api;

[Route("api/[controller]")]
[ApiController]
public class ChatBotController : ControllerBase
{
    [HttpPost("answer")]
    public async Task<IActionResult> Get(ChatGPTDto dto)
    {
        GooglePalmClient client = new GooglePalmClient("AIzaSyCiAA1m4u9_SE2pNFP2_VE0GS4_sTJjVdg");
        // var response = await client.GenerateTextAsync(question);
        List<PalmChatMessage> messages = new()
            {
                 new(dto.Question, "0"),
            };

        var response = await client.ChatAsync(messages, null, null);
        return Ok(response.Candidates[0].Content);
    }
}
