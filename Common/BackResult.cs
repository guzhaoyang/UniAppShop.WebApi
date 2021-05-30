using System;

namespace Common
{
    public enum BackResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 失败
        /// </summary>
        Fail = 0,
        /// <summary>
        /// 错误
        /// </summary>
        Error = -1,
        /// <summary>
        /// 异常
        /// </summary>
        Exception = -2
    }
}
