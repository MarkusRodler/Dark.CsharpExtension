// namespace Dark.CsharpExtension;

// public class Either<TLeft, TRight>
// {
//     readonly TLeft? left;
//     readonly TRight? right;

//     Either(TLeft left) => this.left = left;

//     Either(TRight right) => this.right = right;

//     public static Either<TLeft, TRight> Left(TLeft left) => new(left);
//     public static Either<TLeft, TRight> Right(TRight right) => new(right);

//     public TResult Match<TResult>(Func<TLeft, TResult> leftFunc, Func<TRight, TResult> rightFunc)
//         => right is not null ? rightFunc(right) : leftFunc(left!);
// }
