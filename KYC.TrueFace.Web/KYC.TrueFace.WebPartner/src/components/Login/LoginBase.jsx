import { ShieldCheck, Check } from "lucide-react"

export function LoginBase(props) {
  return(
    <div className="
      relative
      min-h-screen
      bg-base
      flex
      items-stretch
    ">
      {/*
        LEFT PANE — brand identity.
        Hidden on small screens (form takes the whole viewport).
        Uses a gradient background + decorative grid pattern +
        glow blobs to create depth without raster images.
        Three sections stacked vertically: logo (top),
        headline (middle), trust signals (bottom).
      */}
      <div className="
        hidden
        lg:flex
        relative
        flex-1
        flex-col
        justify-between
        p-12
        overflow-hidden
        bg-gradient-to-br
        from-indigo-950
        via-slate-900
        to-slate-950
      ">
        {/* Decorative dot grid pattern */}
        <div
          aria-hidden="true"
          className="
            absolute
            inset-0
            opacity-40
            pointer-events-none
          "
          style={{
            backgroundImage: "radial-gradient(circle at 1px 1px, rgba(165, 180, 252, 0.15) 1px, transparent 0)",
            backgroundSize: "24px 24px"
          }}
        />

        {/* Glow accent in the top-right corner */}
        <div
          aria-hidden="true"
          className="
            absolute
            -top-20
            -right-20
            w-80
            h-80
            bg-indigo-500
            rounded-full
            blur-3xl
            opacity-30
            pointer-events-none
          "
        />

        {/* Glow accent in the bottom-left */}
        <div
          aria-hidden="true"
          className="
            absolute
            -bottom-32
            -left-20
            w-96
            h-96
            bg-cyan-500
            rounded-full
            blur-3xl
            opacity-15
            pointer-events-none
          "
        />

        {/* Top: logo lockup */}
        <div className="relative flex items-center gap-3">
          <div className="
            w-11
            h-11
            rounded-xl
            bg-indigo-500/20
            border
            border-indigo-300/30
            flex
            items-center
            justify-center
            backdrop-blur-sm
          ">
            <ShieldCheck size={22} className="text-indigo-300" />
          </div>
          <span className="text-lg font-medium text-white tracking-tight">
            KYC TrueFace
          </span>
        </div>

        {/* Middle: headline + value prop */}
        <div className="relative max-w-md">
          <h2 className="
            text-4xl
            font-medium
            text-white
            leading-tight
            tracking-tight
            mb-4
          ">
            Identity verification,{" "}
            <span className="text-indigo-300">simplified</span>.
          </h2>
          <p className="
            text-base
            text-slate-300
            leading-relaxed
          ">
            Real-time facial biometrics for secure, frictionless customer onboarding.
          </p>
        </div>

        {/* Bottom: trust signals */}
        <div className="relative">
          <div className="
            flex
            flex-wrap
            gap-x-6
            gap-y-2
            text-xs
            text-slate-400
          ">
            <div className="flex items-center gap-1.5">
              <Check size={14} className="text-emerald-400" />
              SOC 2 Type II
            </div>
            <div className="flex items-center gap-1.5">
              <Check size={14} className="text-emerald-400" />
              LGPD compliant
            </div>
            <div className="flex items-center gap-1.5">
              <Check size={14} className="text-emerald-400" />
              ISO 27001
            </div>
          </div>
        </div>
      </div>

      {/*
        RIGHT PANE — form area.
        Takes full width on mobile, ~480px on desktop.
        Centered vertically, padded for comfort.
      */}
      <div className="
        flex-1
        flex
        items-center
        justify-center
        px-6
        py-10
        lg:max-w-xl
      ">
        <div className="w-full max-w-sm flex flex-col gap-6">

          {/* Mobile-only mini brand mark (since the left pane is hidden) */}
          <div className="lg:hidden flex items-center justify-center gap-2 mb-2">
            <div className="
              w-9
              h-9
              rounded-lg
              bg-brand/10
              flex
              items-center
              justify-center
            ">
              <ShieldCheck size={18} className="text-brand" />
            </div>
            <span className="text-base font-medium text-fg">
              KYC TrueFace
            </span>
          </div>

          {/* Form header */}
          <div className="flex flex-col gap-1">
            <h1 className="text-2xl font-medium text-fg">
              {props.title}
            </h1>
            <p className="text-sm text-fg-subtle">
              {props.subtitle !== undefined
                ? props.subtitle
                : "Sign in to your account"}
            </p>
          </div>

          {props.children}
        </div>
      </div>
    </div>
  )
}
