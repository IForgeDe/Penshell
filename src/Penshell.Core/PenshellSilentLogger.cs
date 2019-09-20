namespace Penshell.Core
{
    using System;
    using System.Collections.Generic;
    using Serilog;
    using Serilog.Core;
    using Serilog.Events;

    /// <summary>
    /// Silent logger copied from serilog examples.
    /// It logs nothing.
    /// </summary>
    public class PenshellSilentLogger : ILogger
    {
        /// <summary>
        /// The static instance of this type.
        /// </summary>
        public static readonly ILogger Instance = new PenshellSilentLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="PenshellSilentLogger"/> class.
        /// </summary>
        private PenshellSilentLogger()
        {
        }

        /// <inheritdoc />
        [MessageTemplateFormatMethod("messageTemplate")]
        public bool BindMessageTemplate(string messageTemplate, object[] propertyValues, out MessageTemplate? parsedTemplate, out IEnumerable<LogEventProperty>? boundProperties)
        {
            parsedTemplate = null;
            boundProperties = null;
            return false;
        }

        /// <inheritdoc />
        public bool BindProperty(string propertyName, object value, bool destructureObjects, out LogEventProperty? property)
        {
            property = null;
            return false;
        }

        /// <inheritdoc />
        public void Debug(string messageTemplate)
        {
        }

        /// <inheritdoc />
        public void Debug<T>(string messageTemplate, T propertyValue)
        {
        }

        /// <inheritdoc />
        public void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        /// <inheritdoc />
        public void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        /// <inheritdoc />
        public void Debug(string messageTemplate, params object[] propertyValues)
        {
        }

        /// <inheritdoc />
        public void Debug(Exception exception, string messageTemplate)
        {
        }

        /// <inheritdoc />
        public void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
        {
        }

        /// <inheritdoc />
        public void Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        /// <inheritdoc />
        public void Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        /// <inheritdoc />
        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        /// <inheritdoc />
        public void Error(string messageTemplate)
        {
        }

        /// <inheritdoc />
        public void Error<T>(string messageTemplate, T propertyValue)
        {
        }

        /// <inheritdoc />
        public void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        /// <inheritdoc />
        public void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        /// <inheritdoc />
        public void Error(string messageTemplate, params object[] propertyValues)
        {
        }

        /// <inheritdoc />
        public void Error(Exception exception, string messageTemplate)
        {
        }

        /// <inheritdoc />
        public void Error<T>(Exception exception, string messageTemplate, T propertyValue)
        {
        }

        /// <inheritdoc />
        public void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        /// <inheritdoc />
        public void Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        /// <inheritdoc />
        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        /// <inheritdoc />
        public void Fatal(string messageTemplate)
        {
        }

        /// <inheritdoc />
        public void Fatal<T>(string messageTemplate, T propertyValue)
        {
        }

        /// <inheritdoc />
        public void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        /// <inheritdoc />
        public void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        /// <inheritdoc />
        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
        }

        /// <inheritdoc />
        public void Fatal(Exception exception, string messageTemplate)
        {
        }

        /// <inheritdoc />
        public void Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
        {
        }

        /// <inheritdoc />
        public void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        /// <inheritdoc />
        public void Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        /// <inheritdoc />
        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        /// <inheritdoc />
        public ILogger ForContext(ILogEventEnricher enricher) => this;

        /// <inheritdoc />
        public ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers) => this;

        /// <inheritdoc />
        public ILogger ForContext(string propertyName, object value, bool destructureObjects = false) => this;

        /// <inheritdoc />
        public ILogger ForContext<TSource>() => this;

        /// <inheritdoc />
        public ILogger ForContext(Type source) => this;

        /// <inheritdoc />
        public void Information(string messageTemplate)
        {
        }

        /// <inheritdoc />
        public void Information<T>(string messageTemplate, T propertyValue)
        {
        }

        /// <inheritdoc />
        public void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        /// <inheritdoc />
        public void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        /// <inheritdoc />
        public void Information(string messageTemplate, params object[] propertyValues)
        {
        }

        /// <inheritdoc />
        public void Information(Exception exception, string messageTemplate)
        {
        }

        /// <inheritdoc />
        public void Information<T>(Exception exception, string messageTemplate, T propertyValue)
        {
        }

        /// <inheritdoc />
        public void Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        /// <inheritdoc />
        public void Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        /// <inheritdoc />
        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        /// <inheritdoc />
        public bool IsEnabled(LogEventLevel level) => false;

        /// <inheritdoc />
        public void Verbose(string messageTemplate)
        {
        }

        /// <inheritdoc />
        public void Verbose<T>(string messageTemplate, T propertyValue)
        {
        }

        /// <inheritdoc />
        public void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        /// <inheritdoc />
        public void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        /// <inheritdoc />
        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
        }

        /// <inheritdoc />
        public void Verbose(Exception exception, string messageTemplate)
        {
        }

        /// <inheritdoc />
        public void Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
        {
        }

        /// <inheritdoc />
        public void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        /// <inheritdoc />
        public void Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        /// <inheritdoc />
        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        /// <inheritdoc />
        public void Warning(string messageTemplate)
        {
        }

        /// <inheritdoc />
        public void Warning<T>(string messageTemplate, T propertyValue)
        {
        }

        /// <inheritdoc />
        public void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        /// <inheritdoc />
        public void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        /// <inheritdoc />
        public void Warning(string messageTemplate, params object[] propertyValues)
        {
        }

        /// <inheritdoc />
        public void Warning(Exception exception, string messageTemplate)
        {
        }

        /// <inheritdoc />
        public void Warning<T>(Exception exception, string messageTemplate, T propertyValue)
        {
        }

        /// <inheritdoc />
        public void Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        /// <inheritdoc />
        public void Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        /// <inheritdoc />
        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        /// <inheritdoc />
        public void Write(LogEvent logEvent)
        {
        }

        /// <inheritdoc />
        public void Write(LogEventLevel level, string messageTemplate)
        {
        }

        /// <inheritdoc />
        public void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
        {
        }

        /// <inheritdoc />
        public void Write<T0, T1>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        /// <inheritdoc />
        public void Write<T0, T1, T2>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        /// <inheritdoc />
        public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
        }

        /// <inheritdoc />
        public void Write(LogEventLevel level, Exception exception, string messageTemplate)
        {
        }

        /// <inheritdoc />
        public void Write<T>(LogEventLevel level, Exception exception, string messageTemplate, T propertyValue)
        {
        }

        /// <inheritdoc />
        public void Write<T0, T1>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        /// <inheritdoc />
        public void Write<T0, T1, T2>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        /// <inheritdoc />
        public void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }
    }
}
