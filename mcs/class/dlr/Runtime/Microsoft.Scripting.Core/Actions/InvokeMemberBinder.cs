/* ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation. 
 *
 * This source code is subject to terms and conditions of the Microsoft Public License. A 
 * copy of the license can be found in the License.html file at the root of this distribution. If 
 * you cannot locate the  Microsoft Public License, please send an email to 
 * dlr@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
 * by the terms of the Microsoft Public License.
 *
 * You must not remove this notice, or any other, from this software.
 *
 *
 * ***************************************************************************/

using System.Dynamic.Utils;

namespace System.Dynamic {

    /// <summary>
    /// Represents the invoke member dynamic operation at the call site,
    /// providing the binding semantic and the details about the operation.
    /// </summary>
    public abstract class InvokeMemberBinder : DynamicMetaObjectBinder {
        private readonly string _name;
        private readonly bool _ignoreCase;
        private readonly CallInfo _callInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvokeMemberBinder" />.
        /// </summary>
        /// <param name="name">The name of the member to invoke.</param>
        /// <param name="ignoreCase">true if the name should be matched ignoring case; false otherwise.</param>
        /// <param name="callInfo">The signature of the arguments at the call site.</param>
        protected InvokeMemberBinder(string name, bool ignoreCase, CallInfo callInfo) {
            ContractUtils.RequiresNotNull(name, "name");
            ContractUtils.RequiresNotNull(callInfo, "callInfo");

            _name = name;
            _ignoreCase = ignoreCase;
            _callInfo = callInfo;
        }

        /// <summary>
        /// The result type of the operation.
        /// </summary>
        public override sealed Type ReturnType {
            get { return typeof(object); }
        }

        /// <summary>
        /// Gets the name of the member to invoke.
        /// </summary>
        public string Name {
            get {
                return _name;
            }
        }

        /// <summary>
        /// Gets the value indicating if the string comparison should ignore the case of the member name.
        /// </summary>
        public bool IgnoreCase {
            get {
                return _ignoreCase;
            }
        }

        /// <summary>
        /// Gets the signature of the arguments at the call site.
        /// </summary>
        public CallInfo CallInfo {
            get { return _callInfo; }
        }

        /// <summary>
        /// Performs the binding of the dynamic invoke member operation.
        /// </summary>
        /// <param name="target">The target of the dynamic invoke member operation.</param>
        /// <param name="args">An array of arguments of the dynamic invoke member operation.</param>
        /// <returns>The <see cref="DynamicMetaObject"/> representing the result of the binding.</returns>
        public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args) {
            ContractUtils.RequiresNotNull(target, "target");
            ContractUtils.RequiresNotNullItems(args, "args");

            return target.BindInvokeMember(this, args);
        }

        // this is a standard DynamicMetaObjectBinder
        internal override sealed bool IsStandardBinder {
            get {
                return true;
            }
        }

        /// <summary>
        /// Performs the binding of the dynamic invoke member operation if the target dynamic object cannot bind.
        /// </summary>
        /// <param name="target">The target of the dynamic invoke member operation.</param>
        /// <param name="args">The arguments of the dynamic invoke member operation.</param>
        /// <returns>The <see cref="DynamicMetaObject"/> representing the result of the binding.</returns>
        public DynamicMetaObject FallbackInvokeMember(DynamicMetaObject target, DynamicMetaObject[] args) {
            return FallbackInvokeMember(target, args, null);
        }

        /// <summary>
        /// When overridden in the derived class, performs the binding of the dynamic invoke member operation if the target dynamic object cannot bind.
        /// </summary>
        /// <param name="target">The target of the dynamic invoke member operation.</param>
        /// <param name="args">The arguments of the dynamic invoke member operation.</param>
        /// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
        /// <returns>The <see cref="DynamicMetaObject"/> representing the result of the binding.</returns>
        public abstract DynamicMetaObject FallbackInvokeMember(DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion);

        /// <summary>
        /// When overridden in the derived class, performs the binding of the dynamic invoke operation if the target dynamic object cannot bind.
        /// </summary>
        /// <param name="target">The target of the dynamic invoke operation.</param>
        /// <param name="args">The arguments of the dynamic invoke operation.</param>
        /// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
        /// <returns>The <see cref="DynamicMetaObject"/> representing the result of the binding.</returns>
        /// <remarks>
        /// This method is called by the target when the target implements the invoke member operation
        /// as a sequence of get member, and invoke, to let the <see cref="DynamicMetaObject"/>
        /// request the binding of the invoke operation only.
        /// </remarks>
        public abstract DynamicMetaObject FallbackInvoke(DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion);
    }
}
