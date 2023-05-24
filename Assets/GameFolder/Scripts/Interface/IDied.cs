using System.Collections;

public interface IDied
{
    public void DieEffect();
    public void Respawn();

    public IEnumerator Dissolve(DissolvingMaterial dissolvingMaterial);
    public IEnumerator Visibled(DissolvingMaterial dissolvingMaterial);
}
