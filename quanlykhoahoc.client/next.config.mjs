/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true,
  async rewrites() {
    return [
      {
        source: "/api/:path*",
        destination: `${
          process.env.NODE_ENV == "development"
            ? process.env.WEBSITE_URL_DEV
            : process.env.WEBSITE_URL_RES
        }/api/:path*`,
      },
    ];
  },
};

export default nextConfig;
