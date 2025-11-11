using Infra.Library;
using Infra.Library.Data.Interface;
using Infra.Library.Data.Repositry;
using Microsoft.EntityFrameworkCore;
using Service.Library;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TutorCollegeProjectContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("constr")));
builder.Services.AddScoped<IStudent, StudentRepository>();
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<ITutors, TutorRepository>();
builder.Services.AddScoped<TutorService>();
builder.Services.AddScoped<IBatch, BatchRepository>();
builder.Services.AddScoped<BatchService>();
builder.Services.AddScoped<IEnrollment , EnrollmentRepository>();
builder.Services.AddScoped<EnrollmentService>();
builder.Services.AddScoped<IPayment, PaymentRepository>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<ISubject, SubjectRepository>();
builder.Services.AddScoped<SubjectService>();
builder.Services.AddScoped<IRegistration,  RegistrationRepository>();
builder.Services.AddScoped<RegistrationService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
