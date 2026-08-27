import { useState, useEffect, useRef } from 'react';

const TYPES = {
  error: {
    bar: 'bg-danger',
    iconBg: 'bg-danger/15',
    iconColor: 'text-danger-light',
    icon: <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M10 14l2-2m0 0l2-2m-2 2l-2-2m2 2l2 2m7-2a9 9 0 11-18 0 9 9 0 0118 0z" />,
  },
  success: {
    bar: 'bg-success',
    iconBg: 'bg-success/15',
    iconColor: 'text-success-light',
    icon: <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z" />,
  },
  warning: {
    bar: 'bg-warning',
    iconBg: 'bg-warning/15',
    iconColor: 'text-warning-light',
    icon: <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z" />,
  },
  info: {
    bar: 'bg-info',
    iconBg: 'bg-info/15',
    iconColor: 'text-info',
    icon: <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />,
  },
};

const Popup = ({ message, type = 'info', duration = 5000, onClose }) => {
  const [entered, setEntered] = useState(false);
  const onCloseRef = useRef(onClose);
  onCloseRef.current = onClose;
  const styles = TYPES[type] ?? TYPES.info;

  const dismiss = () => {
    setEntered(false);
    setTimeout(() => onCloseRef.current?.(), 300);
  };

  useEffect(() => {
    const raf = requestAnimationFrame(() => setEntered(true));
    return () => cancelAnimationFrame(raf);
  }, []);

  useEffect(() => {
    const timer = setTimeout(dismiss, duration);
    return () => clearTimeout(timer);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [duration]);

  return (
    <div
      className={`w-80 transition-all duration-300 ease-out ${
        entered ? 'opacity-100 translate-x-0' : 'opacity-0 translate-x-8'
      }`}
    >
      <div className="relative bg-surface text-fg px-4 py-3 rounded-xl shadow-2xl flex items-center gap-3 border border-divider/40 overflow-hidden">

        <div className={`absolute left-0 top-0 h-full w-1 ${styles.bar}`} />

        <div className={`${styles.iconBg} p-2 rounded-lg ml-2 shrink-0`}>
          <svg className={`w-5 h-5 ${styles.iconColor}`} fill="none" stroke="currentColor" viewBox="0 0 24 24">
            {styles.icon}
          </svg>
        </div>

        <p className="text-sm font-medium text-fg-muted flex-1 leading-snug">{message}</p>

        <button
          onClick={dismiss}
          className="hover:bg-raised p-1 rounded-lg transition-colors cursor-pointer shrink-0"
        >
          <svg className="w-4 h-4 text-fg-subtle" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M6 18L18 6M6 6l12 12" />
          </svg>
        </button>
      </div>
    </div>
  );
};

export default Popup;
