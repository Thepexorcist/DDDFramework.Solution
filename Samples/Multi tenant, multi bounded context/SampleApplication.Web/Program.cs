using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SampleApplication.Web.Configurations;
using SampleApplication.Web.Data;
using SecondContext.Application.IntegrationEventHandlers;
using SecondContext.Application.IntegrationEvents.Incoming;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddAndConfigureFirstContextDatabase(builder.Configuration);
builder.Services.AddAndConfigureSecondContextDatabase(builder.Configuration);
builder.Services.AddAndConfigureEventBus(builder.Configuration);
builder.Services.AddAndConfigureFirstContextEventualConsistency(builder.Configuration);

var configuration = builder.Configuration;

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new SampleApplication.Web.AutofacModules.FirstContextModules.ApplicationModule(configuration)));
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new SampleApplication.Web.AutofacModules.FirstContextModules.MediatorModule(configuration)));
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new SampleApplication.Web.AutofacModules.SecondContextModules.ApplicationModule(configuration)));
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new SampleApplication.Web.AutofacModules.SecondContextModules.MediatorModule(configuration)));


var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<ProjectRegisteredIntegrationEvent, ProjectRegisteredIntegrationEventHandler>();
eventBus.Subscribe<ProjectPublishedIntegrationEvent, ProjectPublishedIntegrationEventHandler>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
