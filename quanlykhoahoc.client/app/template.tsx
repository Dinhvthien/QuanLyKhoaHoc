"use client";

import React from "react";
import { ToastContainer } from "react-toastify";
import { MantineProvider } from "@mantine/core";
import { ModalsProvider } from "@mantine/modals";

export default function Template({ children }: { children: React.ReactNode }) {
  return (
    <>
      <ToastContainer />
      <MantineProvider>
        <ModalsProvider />
        {children}
      </MantineProvider>
    </>
  );
}
