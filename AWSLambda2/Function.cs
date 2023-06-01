using Amazon.Lambda.Core;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambda2;

public class Function
{
    
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<string> FunctionHandler(Persona persona
        , ILambdaContext context)
    {
        MailMessage message = new MailMessage();
        //FROM: CUENTA DEL SENDER DE AWS
        message.From = new MailAddress("EMAIL");
        message.To.Add(new MailAddress(persona.Email));
        message.Subject = "Mensaje desde Lambda";
        message.Body = "Esto es un mensaje de prueba";
        //CONFIGURAMOS LAS CREDENCIALES DE NUESTRO SERVICIO
        //ESTO CON GMAIL CAMBIA, DEBEMOS UTILIZAR EL TOKEN
        NetworkCredential credentials =
            new NetworkCredential("USER", "PASS");
        //CONFIGURAMOS EL SERVIDOR SMTP
        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Host = "server.smtp";
        smtpClient.Port = 25;
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = true;
        smtpClient.Credentials = credentials;
        await smtpClient.SendMailAsync(message);

        string data = JsonConvert.SerializeObject(persona);
        return "Todo OK, Jose Luis " + data;
    }
}
