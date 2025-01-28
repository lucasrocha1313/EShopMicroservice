import type { NextConfig } from "next";

import next_intl from "next-intl/plugin";

const withNextIntl = next_intl();

const nextConfig: NextConfig = {
  /* config options here */
};

export default withNextIntl(nextConfig);
