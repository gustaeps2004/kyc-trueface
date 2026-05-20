import { createContext, useContext, useState, useCallback } from 'react';
import Popup from '../ui/Popup';

const NotificationContext = createContext(null);

export function NotificationProvider({ children }) {
  const [items, setItems] = useState([]);

  const notify = useCallback(({ message, type = 'info', duration = 5000 }) => {
    const id = Date.now() + Math.random();
    setItems(prev => [...prev, { id, message, type, duration }]);
  }, []);

  const remove = useCallback((id) => {
    setItems(prev => prev.filter(n => n.id !== id));
  }, []);

  return (
    <NotificationContext.Provider value={{ notify }}>
      {children}
      <div className="fixed bottom-6 right-6 z-50 flex flex-col gap-3 items-end">
        {items.map(n => (
          <Popup key={n.id} {...n} onClose={() => remove(n.id)} />
        ))}
      </div>
    </NotificationContext.Provider>
  );
}

export function useNotification() {
  const ctx = useContext(NotificationContext);
  if (!ctx) throw new Error('useNotification must be used inside NotificationProvider');
  return ctx;
}
