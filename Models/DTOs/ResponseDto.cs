using System;
using System.Net;

namespace DevsTutorialCenterAPI.Models.DTOs
{
	public class ResponseDto<T>
	{
		public int Code { get; set; }
		public string Message { get; set; }
		public object Data { get; set; }
		public string Error { get; set; }
    }
}

