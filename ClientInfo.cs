using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
  class ClientInfo : IPlugin
  {
    protected static Dictionary<String, int> statDictionary = null;
    public ClientInfo()
    {
      if (statDictionary == null)
      {
        statDictionary = new Dictionary<String, int>();

      }
    }

    public void PreProcessing(HTTPRequest request)
    {
      if (statDictionary.ContainsKey(request.Url))
      {
        statDictionary[request.Url] = (int)statDictionary[request.Url] + 1;
      }
      else
      {
        statDictionary[request.Url] = 1;
      }
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
      sb.Append("<html><body><h1>CLIENT INFO</h1>");
      String[] ip = request.getPropertyByKey("RemoteEndPoint").Split(":");
      sb.Append("<h3>" + "Client IP Address: " + "<span>" + ip[0] + "</span>" +"</h3>");
      sb.Append("<h3>" + "Client Port: "+ "<span>" + ip[1] + "/<span>"+ "</h3>");
      sb.Append("<h3>" + "Browser Information: "+ "<span>" + request.getPropertyByKey("user-agent") + "/<span>"+ "</h3>");
      sb.Append("<h3>" + "Accept-Charset: "+ "<span>" + request.getPropertyByKey("accept-language")+ "/<span>" + "</h3>");
      sb.Append("<h3>" + "Accept-Encoding: "+ "<span>" + request.getPropertyByKey("accept-encoding")+ "/<span>" + "</h3>");
      sb.Append("</body></html>");
      response = new HTTPResponse(200);
      response.body = Encoding.UTF8.GetBytes(sb.ToString());
      return response;
    }
    public HTTPResponse PostProcessing(HTTPResponse response)
    {
      throw new NotImplementedException();
    }
  }
}