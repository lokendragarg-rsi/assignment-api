using Assignment.Framework.Builder.Abstraction;

namespace Assignment.Framework.Builder.Factory;

/// <summary>
/// 
/// </summary>
public interface IResponseBuilderFactory
{
    /// <summary>
    /// Gets the builder.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IResponseBuilder<T> GetBuilder<T>();
}
