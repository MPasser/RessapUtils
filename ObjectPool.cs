using System.Collections.Generic;

namespace Ressap.Utils {
    public abstract class ObjectPool<T> {
        private Queue<T> queue = new Queue<T>();
        private int capacity = -1;

        public void SetCapacity(int capacity) {
            this.capacity = capacity;
        }

        public T Get() {
            T obj;
            if (queue.Count > 0) {
                obj = queue.Dequeue();
            } else {
                obj = InstantiateObj();
            }

            OnInstantiate(obj);

            return obj;
        }

        public void Collect(T obj) {
            if (null == obj) {
                return;
            }

            OnCollected(obj);

            if (capacity >= 0 && queue.Count >= capacity) {
                DisposeObj(obj);
            } else {
                queue.Enqueue(obj);
            }
        }

        protected abstract void OnInstantiate(T obj);
        protected abstract void OnCollected(T obj);

        protected abstract T InstantiateObj();
        protected abstract void DisposeObj(T obj);
    }
}