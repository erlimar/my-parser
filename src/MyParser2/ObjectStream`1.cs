using System;

namespace MyParser2
{
    /// <summary>
    /// Representa um <see cref="System.IO.Stream"/> de objetos
    /// de entrada em um passo da compilação.
    /// </summary>
    public class ObjectStream<T>
    {
        public void Discard(MyDiscardDelegate<T> ignore)
        {
            if (ignore == null) return;

            while (!EndOfStream())
            {
                long pos = GetPosition();
                T element = Next();

                if (!ignore(element))
                {
                    // Volta a posição ao que era antes de obter o próximo elemento
                    SetPosition(pos);
                    break;
                }
            }
        }

        /// <summary>
        /// Retorna se está ou não no final do stream
        /// </summary>
        /// <returns>Retorna <see cref="true"/> se estiver no final. Ao contrário retorna <see cref="false"/></returns>
        public virtual bool EndOfStream()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retorna o próximo elemento e modifica a posição
        /// </summary>
        /// <returns>Elemento na posição atual</returns>
        public virtual T Next()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtem a posição do elemento atual
        /// </summary>
        /// <returns>Posição do elemento atual</returns>
        public virtual long GetPosition()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Atribui a posição para o elemento atual
        /// </summary>
        /// <param name="position">Nova posição</param>
        public virtual void SetPosition(long position)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adiciona um novo elemento no final do stream
        /// </summary>
        /// <param name="element">Elemento a adicionar</param>
        public virtual void Push(T element)
        {
            throw new NotImplementedException();
        }
    }
}
