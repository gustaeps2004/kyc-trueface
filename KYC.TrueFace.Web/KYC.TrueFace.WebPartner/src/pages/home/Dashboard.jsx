import Layout from "../../components/base/Layout";
import {
  Search,
  XCircle,
  CheckCircle2,
  Clock,
  ThumbsUp,
  ThumbsDown,
} from "lucide-react";

export function Dashboard() {
  const cards = [
    {
      title: "Consulted in the last week",
      value: 34,
      variant: "accent",
      icon: <Search size={18} />,
    },
    {
      title: "Reproved in the last week",
      value: 15,
      variant: "danger",
      icon: <XCircle size={18} />,
    },
    {
      title: "Approved in the last week",
      value: 19,
      variant: "success",
      icon: <CheckCircle2 size={18} />,
    },
    {
      title: "Pending manual action",
      value: 10,
      variant: "warning",
      icon: <Clock size={18} />,
    },
    {
      title: "Approved manually in the last month",
      value: 15,
      variant: "success",
      icon: <ThumbsUp size={18} />,
    },
    {
      title: "Reproved manually in the last month",
      value: 3,
      variant: "danger",
      icon: <ThumbsDown size={18} />,
    },
  ];

  const variantStyles = {
    accent: {
      border: "border-l-accent",
      value: "text-accent-light",
      icon: "text-accent-light bg-accent/10",
    },
    success: {
      border: "border-l-success",
      value: "text-success-light",
      icon: "text-success-light bg-success/10",
    },
    danger: {
      border: "border-l-danger",
      value: "text-danger-light",
      icon: "text-danger-light bg-danger/10",
    },
    warning: {
      border: "border-l-warning",
      value: "text-warning-light",
      icon: "text-warning-light bg-warning/10",
    },
  };

  return (
    <Layout name="Welcome, Gustavo">
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-5">
        {cards.map((card, index) => {
          const styles = variantStyles[card.variant];
          return (
            <div
              key={index}
              className={`
                bg-surface
                border
                border-divider/30
                border-l-2
                ${styles.border}
                rounded-xl
                p-6
                transition-all
                duration-200
                hover:border-divider/60
                hover:-translate-y-0.5
              `}
            >
              <div className="flex items-start justify-between mb-4">
                <p className="text-xs text-fg-subtle uppercase tracking-wide leading-tight max-w-[80%]">
                  {card.title}
                </p>
                <div className={`${styles.icon} rounded-lg p-2 flex items-center justify-center`}>
                  {card.icon}
                </div>
              </div>
              <p className={`text-4xl font-medium leading-none ${styles.value}`}>
                {card.value}
              </p>
            </div>
          );
        })}
      </div>
    </Layout>
  );
}
