using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Quartz.Impl;
using Quartz;
using WebApplication3.DataBase;
using WebApplication3.Quartz;

namespace WebApplication3
{
    public class Program
    {

        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);//��������� ��� �������� �������� 

            // ��������� ������� � ���������
            builder.Services.AddControllers();

            //��������� �������
            builder.Services.AddDbContext<AppDbContext>();

            // ��������� ������� � �������
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiDb"});
            });

            var app = builder.Build();

            //��������� ������� ��� ��������� ������������ 
            if (app.Environment.IsDevelopment())//���� � ������ ���������� , �� true
            {
                app.UseSwagger();
              app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiDb");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            //���������� ������������ , � ������������ ��������� ��������
            app.MapControllers();
            Trigger.trigger().Wait();
            TG tg = new TG();
            tg.start();
            app.Run();
            
        }
    }
}
