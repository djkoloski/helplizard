public static class Layers
{
	public const int IgnoreRaycasts = 2;
	public const int IgnoreRaycastsMask = (1 << IgnoreRaycasts);
	public const int NoCollide = 8;
	public const int NoCollideMask = (1 << NoCollide);

	public const int AllIgnoreRaycasts = IgnoreRaycastsMask | NoCollideMask;
}