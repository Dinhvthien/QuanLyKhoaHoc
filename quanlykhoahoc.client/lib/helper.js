"use client";

import { toast } from "react-toastify";
import { useMemo } from "react";
import { useRouter, useSearchParams } from "next/navigation";
import queryString from "query-string";
import { useDebouncedCallback } from "@mantine/hooks";

async function TryFetch(action) {
  try {
    await action();
  } catch (error) {
    if (error.response) {
      try {
        const errors = JSON.parse(error.response).errors;
        for (const title in errors) {
          toast.warning(...errors[title]);
        }
      } catch (e) {
        toast.error("Lỗi Gì Đó");
      }
    }

    throw error;
  }
}

export async function handleSubmit(action, message) {
  toast.promise(TryFetch(action), {
    success: message,
    error: "Vui Lòng Kiểm Tra Lại Thông Tin",
    pending: "Đang Tải ...",
  });
}

export function useQuery() {
  const searchParams = useSearchParams();

  const { filters, sorts, page, pageSize } = useMemo(() => {
    return Object.fromEntries(searchParams.entries());
  }, [searchParams]);

  const query = useMemo(() => {
    return {
      filters,
      sorts: sorts,
      page: page,
      pageSize: pageSize,
    };
  }, [filters, sorts, page, pageSize]);

  return query;
}

export const useFilter = () => {
  const router = useRouter();
  const searchParams = useSearchParams();

  const updateFilter = useDebouncedCallback((field, val) => {
    const url = new URLSearchParams(searchParams);

    if (val) {
      url.set(field, val);
    } else {
      url.delete(field);
    }

    router.push(`?${url.toString()}`);
  }, 1000);

  return updateFilter;
};

export function useSort() {
  const router = useRouter();

  const handleSort = useMemo(() => {
    return (field, remove = null) => {
      const parsed = queryString.parse(window.location.search);
      let sorts = parsed.sorts ? parsed.sorts.split(",") : [];

      const index = sorts.findIndex(
        (sort) => sort === field || sort === `-${field}`
      );

      if (index !== -1) {
        if (remove) {
          sorts.splice(index, 1);
        } else {
          sorts[index] = sorts[index].startsWith("-")
            ? sorts[index].substr(1)
            : `-${sorts[index]}`;
        }
      } else {
        sorts.push(`-${field}`);
      }

      const newSorts = sorts.join(",");
      const newQuery = { ...parsed, sorts: newSorts || undefined };
      const newQueryString = queryString.stringify(newQuery);
      router.push(`?${newQueryString.replace(/%2C/g, ",")}`);
    };
  }, [router]);

  return handleSort;
}

export function formatCurrencyVND(amount) {
  return amount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
}