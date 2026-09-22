import Layout from "@/shared/layout/Layout";
import {
  Search,
  XCircle,
  CheckCircle2,
  Clock,
  ThumbsUp,
  ThumbsDown,
} from "lucide-react";
import { useCallback, useEffect, useState } from "react";
import { useTranslation } from 'react-i18next';
import { useApi } from "@/shared/hooks/useApi";
import { GetTokenData } from "@/shared/utils/getTokenData";
import { dashboardService } from "../api/dashboardService";

// summaryKey matches the field of the same name on GET /v1/dashboard/summary.
const cards = [
  {
    titleKey: "dashboard.consultedLastWeek",
    summaryKey: "consultedLastWeek",
    variant: "accent",
    icon: <Search size={18} />,
  },
  {
    titleKey: "dashboard.reprovedLastWeek",
    summaryKey: "reprovedLastWeek",
    variant: "danger",
    icon: <XCircle size={18} />,
  },
  {
    titleKey: "dashboard.approvedLastWeek",
    summaryKey: "approvedLastWeek",
    variant: "success",
    icon: <CheckCircle2 size={18} />,
  },
  {
    titleKey: "dashboard.pendingManual",
    summaryKey: "pendingManualReview",
    variant: "warning",
    icon: <Clock size={18} />,
  },
  {
    titleKey: "dashboard.approvedManuallyLastMonth",
    summaryKey: "approvedManuallyLastMonth",
    variant: "success",
    icon: <ThumbsUp size={18} />,
  },
  {
    titleKey: "dashboard.reprovedManuallyLastMonth",
    summaryKey: "reprovedManuallyLastMonth",
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

export function Dashboard() {
  const [summary, setSummary] = useState(null);
  const { execute, isLoading } = useApi();
  const { t } = useTranslation();
  const loggedName = GetTokenData()?.user_name?.split(' ')[0];

  const handlerSummary = useCallback(async (signal) => {
    await execute(
      () => dashboardService.getSummary(signal),
      { onSuccess: (response) => setSummary(response.data) }
    );
  }, [execute]);

  useEffect(() => {
    const controller = new AbortController()
    handlerSummary(controller.signal)
    return () => controller.abort()
  }, [handlerSummary]);

  return (
    <Layout name={t('dashboard.welcome', { name: loggedName })}>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-5">
        {cards.map((card) => {
          const styles = variantStyles[card.variant];
          const value = summary?.[card.summaryKey];
          return (
            <div
              key={card.summaryKey}
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
                  {t(card.titleKey)}
                </p>
                <div className={`${styles.icon} rounded-lg p-2 flex items-center justify-center`}>
                  {card.icon}
                </div>
              </div>
              {isLoading ? (
                <span className="block h-9 w-14 rounded bg-divider/30 animate-pulse" />
              ) : (
                <p className={`text-4xl font-medium leading-none ${styles.value}`}>
                  {value ?? "-"}
                </p>
              )}
            </div>
          );
        })}
      </div>
    </Layout>
  );
}
