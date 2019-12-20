using System;
using System.Diagnostics.CodeAnalysis;

namespace DotNext.Net.Cluster.Consensus.Raft.Http
{
    internal interface IHttpClientMetrics
    {
        void ReportResponseTime(TimeSpan value);
    }

    /// <summary>
    /// Contains a set of callbacks that can be used to report
    /// runtime metrics generated by HTTP-specific implementation of Raft cluster node.
    /// </summary>
    /// <remarks>
    /// You need to register singleton service of type <see cref="MetricsCollector"/>
    /// to collect metrics produced by Raft node.
    /// </remarks>
    [SuppressMessage("Security", "CA2119", Justification = "Internal interfaces for convenient programming only")]
    public class HttpMetricsCollector : MetricsCollector, IHttpClientMetrics
    {
        /// <summary>
        /// Reports about HTTP response time.
        /// </summary>
        /// <param name="value">The response time.</param>
        public virtual void ReportResponseTime(TimeSpan value)
        {
        }
    }
}