using UnityEngine;

public static class CoroutineExtension
{
    public static void KillCoroutine( this MonoBehaviour coroutineSource, ref Coroutine coroutine )
    {
        if( coroutine == null || coroutineSource == null ) return;

        coroutineSource.StopCoroutine( coroutine );
        coroutine = null;
    }
}
